# 发票智能识别与处理系统

基于 .NET 8 WinForms 的发票自动识别与批量处理工具，支持 **PDF 图像识别** 和 **XML 结构化解析** 两种模式，集成百度 OCR 与 OpenCV 模板匹配技术，实现发票信息的自动提取、分类与批量导出。

## ✨ 功能特性

| 功能 | 说明 |
|------|------|
| 📄 **双模识别** | 支持 PDF（OCR+模板匹配）和 XML（XPath 解析）两种发票格式 |
| 🤖 **智能 OCR** | 集成百度 OCR API，支持标准/高精度两种识别模型切换 |
| 🎯 **模板匹配** | 基于 OpenCV 的图像模板匹配，自动定位关键字段区域 |
| 📋 **灵活模板** | 可视化模板编辑器，支持 ROI 框选与 XPath 节点配置 |
| 📊 **批量导出** | 一键导出 Excel 汇总表，自动重命名并归档 PDF 文件 |
| 🗂️ **分类管理** | 多层级发票类型分类，支持增删改查与独立模板配置 |
| 🔐 **密钥管理** | 安全的 API 密钥存储机制，支持本地加密保存 |
| 🔄 **异步处理** | 后台线程执行识别任务，实时进度条与 UI 响应 |

## 🖥️ 系统要求

- **运行环境**: .NET 8.0 Runtime 或更高版本
- **操作系统**: Windows 7 SP1+ / Windows 10 / Windows 11
- **内存要求**: 建议 4GB 以上（图像处理需要）
- **网络**: 需要联网调用百度 OCR API
- **百度账号**: 需拥有百度智能云账号并开通 OCR 服务

## 📦 依赖库

| 包名 | 版本 | 用途 |
|------|------|------|
| `SunnyUI` | 最新版 | WinForms UI 控件库 |
| `EPPlus` | 8.x | Excel 文件读写 |
| `OpenCvSharp4` | 4.13.0 | OpenCV C# 绑定，图像处理 |
| `OpenCvSharp4.Extensions` | 4.13.0 | Bitmap 与 Mat 互转 |
| `OpenCvSharp4.runtime.win` | 4.13.0 | Windows 运行时 |
| `PdfPig` | 0.1.14 | PDF 文本提取与解析 |
| `Spire.PDF` | 12.x | PDF 转图像 |
| `System.Drawing.Common` | 8.x | GDI+ 图像操作 |

## 🚀 快速开始

### 1. 环境准备

确保已安装 .NET 8.0 SDK 或 Runtime：

```bash
dotnet --version  # 确认版本 >= 8.0
```

### 2. 配置百度 OCR API

1. 登录 [百度智能云控制台](https://cloud.baidu.com/)
2. 创建应用并获取 **API Key** 和 **Secret Key**
3. 在程序界面输入密钥，或勾选"存储密钥"自动保存到本地

### 3. 目录结构准备

```
程序根目录/
├── Templates/
│   ├── PDF/                    # PDF 模板根目录
│   │   └── {模板名称}/         # 如：增值税发票、普通发票
│   │       ├── {模板名}.json   # 模板配置文件
│   │       ├── Config.txt      # Excel 输出格式配置
│   │       ├── ConfigPDF.txt   # PDF 重命名格式配置
│   │       └── {字段图片}.png  # 模板匹配用截图
│   └── XML/                    # XML 模板根目录
│       └── {模板名称}/
│           └── {模板名}.json   # XPath 配置
├── appconfig.json              # 应用程序配置（自动创建）
└── Api.json                    # API 密钥存储（可选）
```

### 4. 启动程序

```bash
dotnet run
# 或编译后运行
 dotnet build -c Release
 ./bin/Release/net8.0-windows/发票.exe
```

## 📖 使用指南

### 主界面操作流程

```
┌─────────────────────────────────────────────────────────────┐
│  ① 选择文件类型(PDF/XML) → ② 选择模板名称 → ③ 加载模板      │
│  ④ 选择发票文件夹 → ⑤ 选择导出目录 → ⑥ 输入 API 密钥       │
│  ⑦ 开始识别 → ⑧ 预览表格 → ⑨ 导出 Excel/PDF                │
└─────────────────────────────────────────────────────────────┘
```

### PDF 模式详细步骤

#### 第一步：制作识别模板

1. 点击 **"制作模板"** 打开模板编辑器
2. 在左侧图像区域：
   - **左键框选**：先框选模板匹配区域（用于定位）
   - **再次框选**：框选 ROI 识别区域（相对模板偏移）
3. 在右侧表格中填写字段名称（如：发票号码、金额、日期）
4. 点击 **"保存"** 生成模板配置

**模板 JSON 格式示例：**
```json
{
  "Templates": [
    {
      "ClassName": "发票号码",
      "ImageFile": "发票号码.png",
      "ROI": "120,80,200,40"
    },
    {
      "ClassName": "开票日期",
      "ImageFile": "开票日期.png",
      "ROI": "450,80,180,40"
    }
  ]
}
```

**ROI 格式**: `X,Y,Width,Height`（相对于模板左上角的像素偏移）

#### 第二步：配置输出格式

在模板目录下创建两个配置文件：

**Config.txt**（Excel 行格式）：
```
增值税发票:{0}_{1}_{2}
普通发票:{0}-{1}
```

**ConfigPDF.txt**（PDF 重命名格式）：
```
增值税发票:发票_{0}_{1}_{2}
普通发票:收据_{0}-{1}
```

- `{0}`, `{1}`... 对应模板中第 1、2 个字段的识别结果
- 占位符数量必须与模板字段数一致

#### 第三步：批量识别

1. 选择包含 PDF 的源文件夹
2. 选择导出目标目录
3. 点击 **"开始识别"**
4. 程序自动执行：
   - 提取 PDF 文本进行发票分类
   - PDF 转高清图像
   - OpenCV 模板匹配定位字段
   - 百度 OCR 识别字段内容
   - 按格式整合结果

### XML 模式详细步骤

XML 模式无需图像处理，直接解析结构化数据：

1. 选择 **文件类型 = XML**
2. 点击 **"制作模板"** 打开 XPath 配置器
3. 左侧显示 XML 所有节点路径和值
4. 选中需要的节点，点击 **"添加节点"**
5. 在右侧配置表中填写字段名称
6. 保存配置后，程序通过 XPath 直接提取值

## 🏗️ 项目架构

```
发票/
├── Program.cs              # 程序入口
├── AppConfig.cs            # 应用配置管理（JSON 持久化）
│
├── Form1.cs / .Designer.cs # 主窗体（协调层）
├── ExcelShow.cs            # Excel 预览与导出窗体
├── MakeModes.cs            # PDF 模板编辑器
├── MakeXMLMode.cs          # XML 模板编辑器
│
├── OCR.cs                  # 百度 OCR 客户端（底层）
├── BaiduOcrSync.cs         # 同步 OCR 服务封装
├── OpterCV.cs              # OpenCV 图像处理工具
├── ZoomImageBox.cs         # 可缩放图像框选控件
│
├── TemplateConfig.cs       # 模板数据模型与配置管理
└── README.md               # 本文件
```

### 核心服务层

| 服务 | 职责 | 关键方法 |
|------|------|----------|
| `OcrService` | OCR 识别调度 | `Recognize(Bitmap)` |
| `TemplateService` | 模板加载解析 | `LoadPDFTempleta()`, `LoadXMLTrmoleta()` |
| `FileExportService` | 安全文件导出 | `CopyFiles()` |


## ⚙️ 配置详解

### 模板配置文件

#### PDF 模板 (`{模板名}.json`)

```json
{
  "Templates": [
    {
      "ClassName": "字段名称（用于显示）",
      "ImageFile": "模板图片文件名.png",
      "ROI": "相对偏移X,相对偏移Y,宽度,高度"
    }
  ]
}
```

#### XML 模板 (`{模板名}.json`)

```json
[
  {
    "ClassName": "字段名称",
    "XPath": "//Invoice/Header/@InvoiceNumber"
  },
  {
    "ClassName": "金额",
    "XPath": "//Invoice/Amount/Total"
  }
]
```

### 输出格式配置

| 文件 | 作用 | 示例 |
|------|------|------|
| `Config.txt` | Excel 每行数据的格式 | `发票_{0}_{1}` → `发票_12345_2024-01` |
| `ConfigPDF.txt` | 导出 PDF 的文件名格式 | `{0}_{1}` → `12345_2024-01.pdf` |

## 🔧 高级功能

### 调试模式

勾选 **"调试模板"** 后，OpenCV 模板匹配会弹出可视化窗口，显示匹配位置红框，便于调整模板。

### 模型切换

使用界面右上角开关切换 OCR 模型：
- **标准模型**：通用场景文字识别，速度较快
- **高精度模型**：高精度文字识别，准确率更高

### 密钥安全存储

勾选 **"存储密钥"** 后，API Key 和 Secret Key 会加密保存到 `Api.json`，下次启动自动加载。

## 🐛 故障排查

| 问题现象 | 可能原因 | 解决方案 |
|----------|----------|----------|
| "API Key 不能为空" | 未输入密钥 | 检查 textBox3/textBox4 或 Api.json |
| "未找到模板" | Templates 目录为空 | 先制作模板或检查目录结构 |
| 模板匹配失败 | 模板图片与发票差异大 | 重新截取更精确的模板区域 |
| OCR 返回空 | 图像模糊/反光 | 提高 PDF 转图像 DPI，或切换高精度模型 |
| 导出 Excel 格式错乱 | 占位符数量不匹配 | 检查 Config.txt 中 `{n}` 数量与模板字段数 |
| 分类错误 | 发票文本不含分类关键词 | 修改分类名称或添加更通用的关键词 |
| 程序闪退 | OpenCV 运行时缺失 | 确保安装 `OpenCvSharp4.runtime.win` |

## 📝 开发说明

### 构建要求

```xml
<!-- 项目文件需包含以下框架引用 -->
<TargetFramework>net8.0-windows</TargetFramework>
<UseWindowsForms>true</UseWindowsForms>
```

### 关键类图

```
Form1 (主窗体)
├── OcrService (OCR服务)
│   └── BaiduOcrSync (百度API封装)
├── TemplateService (模板服务)
│   └── TemplateConfig (配置模型)
├── FileExportService (导出服务)
│
├── MakeModes (PDF模板编辑器)
│   └── ZoomImageBox (自定义图像控件)
├── MakeXMLMode (XML模板编辑器)
└── ExcelShow (Excel导出预览)
    └── EPPlus (Excel库)
```

### 线程安全注意事项

- UI 控件更新需通过 `Invoke()` 回到主线程
- `HttpClient` 使用静态实例避免端口耗尽
- Token 缓存使用 `lock` 防止并发重复获取

## 📄 许可证

MIT License

---


