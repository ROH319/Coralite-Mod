sampler baseTex : register(s0);

float4 coreColor;
float4 lightColor;
float uFlowAdd;
float uTime;
float uFlowUEx;
float uBaseUEx;
float uNormalCadj;
matrix transformMatrix;
texture uCoreImage;
texture uFlowImage;
texture uNormalImage;

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
sampler2D normalTex = sampler_state
{
    texture = <uNormalImage>;
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

    //计算法线贴图的位置
    float2 normalUV = float2(xcoord + uTime, 0.5 - 1 / 6.0 + ycoord / 3 + sin(uTime * 0.5) / 3.0);
    float2 normalC = tex2D(flowTex, normalUV).xy; //同时应用与核心取点偏移
    
    //底图
    float2 st = float2((xcoord * uBaseUEx + uTime) % 1.0, ycoord);
    float4 bColor = tex2D(baseTex, st).xyzw;
    bColor = (bColor * coreColor + float4(bColor.xyz, 0) * bColor.r) * input.Color.r;
    
    //核心叠加
    float2 ct = float2((xcoord / 2 + uTime * 1.4) % 1.0 + (normalC.x - 0.5) * 0.02, ycoord);
    float coreTC = tex2D(coreTex, st).x;
    float4 coreC = lerp(coreColor, lightColor, coreTC + (normalC.x + normalC.y - 1) / 2 * uNormalCadj);
    
    bColor += coreTC * coreC;
    
    //流动图叠加
    ct = float2((xcoord / uFlowUEx - uTime * 0.5 + (normalC.x - 0.5) * 0.01) % 1.0, ycoord);
    bColor += float4(tex2D(flowTex, ct).xyz, 0) * uFlowAdd;
    
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