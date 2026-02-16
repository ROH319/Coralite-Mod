sampler baseTex : register(s0);

float4 coreColor;
float4 lightColor;
float uBottomCA;
float uFlowAdd;
float uTime;
float uFlowUEx;
float uBaseUEx;
float uNormalCadj;
matrix transformMatrix;
texture uCoreImage;
texture uFlowImage;

sampler2D coreTex = sampler_state
{
    texture = <uCoreImage>;
    magfilter = LINEAR;
    minfilter = LINEAR;
    mipfilter = LINEAR;
    AddressU = wrap;
    AddressV = wrap; //循环UV
};
sampler2D flowTex = sampler_state
{
    texture = <uFlowImage>;
    magfilter = LINEAR;
    minfilter = LINEAR;
    mipfilter = LINEAR;
    AddressU = wrap;
    AddressV = wrap; //循环UV
};

struct VertexShaderInput
{
    float4 Position : POSITION;
    float2 TexCoords : TEXCOORD0;
    float4 Color : COLOR0;
};

struct VertexShaderOutput
{
    float4 Position : POSITION;
    float2 TexCoords : TEXCOORD0;
    float4 Color : COLOR0;
};

VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output;
    
    output.Color = input.Color;
    output.TexCoords = input.TexCoords;
    output.Position = mul(input.Position, transformMatrix);

    return output;
}

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
    float xcoord = input.TexCoords.x;
    float ycoord = input.TexCoords.y;

    //计算流动图额外偏移
    float flowcoord = xcoord * uFlowUEx;
    float2 exOffUV = float2(flowcoord + uTime * 0.4f, 0.1 + ycoord * 0.8 + sin(uTime + flowcoord) * 0.1);
    float2 exOffC = tex2D(flowTex, exOffUV).xy; //同时应用与核心取点偏移
    
    float2 ct2 = float2((flowcoord + uTime * 0.5 + (exOffC.x - 0.5) * 0.05) % 1.0, ycoord);

    //底图
    float2 st = float2((xcoord * uBaseUEx + uTime) % 1.0, ycoord);
    float4 bColor = tex2D(flowTex, ct2).x * coreColor * uBottomCA; //深色底色流动图
    float4 baseTexC = tex2D(baseTex, st).xyzw;//基础颜色
    bColor += baseTexC * coreColor * input.Color.r;
    
    //流动图叠加
    float2 ct3 = float2((flowcoord - uTime * 0.5 + (exOffC.x - 0.5) * 0.05) % 1.0, ycoord);
    float normalC2 = tex2D(flowTex, ct3).x;

    //核心叠加
    float2 ct = float2((xcoord / 2 + uTime) % 1.0 + (exOffC.x - 0.5) * 0.02, ycoord);
    float coreTC = tex2D(coreTex, st).x;
    float4 coreC = lerp(coreColor, lightColor, coreTC + clamp((exOffC.x + normalC2 - 0.5) * 2, -1, 1.4) * uNormalCadj);
    
    bColor += coreTC * coreC;
    
    return bColor;
}

technique Technique1
{
    pass MyNamePass
    {
        VertexShader = compile vs_3_0 VertexShaderFunction();
        PixelShader = compile ps_3_0 PixelShaderFunction();
    }
}