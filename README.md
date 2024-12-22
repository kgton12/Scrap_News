# 📚 Scrap_News Project Overview

## 📝 Project Description
The **Scrap_News** project is designed for educational purposes to capture news articles from various websites. It uses web scraping techniques to extract:
- 🌐 URLs
- 📰 Titles
- ✍️ Content  

All extracted data is stored in a **SQLite database** for further analysis.

---

## ✨ Features
- 🌍 **URL Capture**: Retrieves URLs of news articles from specified websites.  
- 📝 **Content Extraction**: Extracts titles and content from the captured URLs.  
- 🗄️ **Database Storage**: Saves the extracted data into a SQLite database.  

---

## 📦 Libraries Used

### 🔧 HtmlAgilityPack
- **Version**: 1.11.42  
- **Description**: A powerful HTML parser for manipulating HTML documents.  
- **Usage**:
  - Loading HTML documents from URLs.  
  - Selecting and extracting specific HTML nodes.  

### 🚗 Selenium.Support
- **Version**: 4.1.0  
- **Description**: Provides additional support classes for Selenium WebDriver.  
- **Usage**: Included but not actively used in the current implementation.  

### 🌐 Selenium.WebDriver
- **Version**: 4.1.0  
- **Description**: Automates browser actions and testing across multiple browsers.  
- **Usage**: Included but not actively used in the current implementation.  

### 🌟 Selenium.WebDriver.ChromeDriver
- **Version**: 100.0.4896.6000  
- **Description**: Implements the WebDriver protocol for Chrome.  
- **Usage**: Included but not actively used in the current implementation.  

### 📂 System.Data.SQLite
- **Version**: 1.0.115.5  
- **Description**: An ADO.NET provider for SQLite databases.  
- **Usage**:
  - Connecting to the SQLite database.  
  - Executing SQL commands for inserting, updating, and retrieving data.  

---

## 🏗️ Project Structure
- **`Program.cs`**: Main application logic including methods for:
  - Capturing URLs  
  - Processing URLs  
  - Extracting content  
  - Database interaction  
- **`Scrap_News.csproj`**: Defines dependencies and target framework.  
- **`Scrap_News.sln`**: Visual Studio solution file.  

---

## 🚀 How to Run the Project

1. **Clone the repository**:  
   ```bash
   git clone https://github.com/yourusername/Scrap_News.git
## 🔧 Restore Dependencies

```bash
dotnet restore
```

## 📇 Build the Project

```bash
dotnet build
```

## 🚀 Run the Project

```bash
dotnet run
```

---

## 🤝 Contribution

Contributions are welcome! Follow these steps:

1. Fork the project.

2. Create a new branch:
   ```bash
   git checkout -b feature/new-feature
   ```
3. Commit your changes:
   ```bash
   git commit -am 'Add new feature'
   ```
4. Push to the branch:
   ```bash
   git push origin feature/new-feature
   ```
5. Create a Pull Request.

---

## 📜 License

This project is licensed under the MIT License.

---

🎉 Happy coding!

