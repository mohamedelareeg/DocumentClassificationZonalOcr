# Document Classification with Zonal OCR

[![Watch the video](https://img.youtube.com/vi/zibVcrxsx9c/maxresdefault.jpg)](https://www.youtube.com/watch?v=zibVcrxsx9c)

This project is a .NET Core 8 web application that facilitates document classification and Zonal OCR (Optical Character Recognition). It allows users to define document types, create fields within these documents (indexing fields), upload sample documents for training, define anchor points to assist in document detection, and perform OCR to extract text from documents based on the defined fields.

## Features

### Document Classification
- **Form Creation:** Users can create document types/forms by defining various fields within them.
- **Anchor Points:** Anchor points can be added to assist in document detection. These anchor points are used as reference points to guide the OCR process.
- **Rectangle Placement:** Rectangles can be added to uploaded images to specify the locations of indexing fields within the document.

### OCR (Optical Character Recognition)
- **OpenCV Integration:** Utilizes OpenCV for image processing and assisting in the OCR process.
- **Multiple OCR Engines:** Supports integration with multiple OCR engines for extracting text from documents.
- **Field Mapping:** Extracted text is assigned to the corresponding indexing fields based on predefined mappings.

## Technologies Used

### Programming Languages
- **C#:** The primary language for backend development.
- **HTML, CSS, JavaScript:** Utilized for frontend development.

### Frameworks and Libraries
- **ASP.NET Core:** Used for building web APIs and MVC applications.
- **Entity Framework Core:** Provides ORM capabilities for interacting with the database.
- **OpenCvSharp:** Integrates OpenCV functionality for image processing tasks.
- **Serilog:** Used for logging within the application.
- **SixLabors.ImageSharp:** Used for image manipulation and processing.
- **Swashbuckle.AspNetCore:** Implements Swagger/OpenAPI for API documentation.
- **Tesseract:** Provides OCR capabilities for text extraction from images.

### Database
- **Microsoft SQL Server:** Utilized as the relational database management system.

### Development Tools
- **Visual Studio 2019:** Used as the integrated development environment (IDE) for project development.
- **Docker:** Used for containerization of the application.

## Project Structure

The project follows a typical ASP.NET Core web application structure:
- **DocumentClassificationZonalOcr.Api:** Contains the API endpoints for interacting with the application.
- **DocumentClassificationZonalOcr.MVC:** Provides the user interface for interacting with the application.
- **DocumentClassificationZonalOcr.Shared:** Contains shared code and utilities used across the solution.

## Dependencies

- **Microsoft.EntityFrameworkCore:** ORM for database interactions.
- **OpenCvSharp4:** OpenCV wrapper for image processing tasks.
- **Serilog.AspNetCore:** Logging framework for ASP.NET Core applications.
- **Swashbuckle.AspNetCore:** Middleware for Swagger/OpenAPI integration.
- **SixLabors.ImageSharp:** Image processing library.
- **Tesseract:** OCR engine for text extraction from images.

## Usage

To use the application:
1. Clone the repository.
2. Open the solution file (`DocumentClassificationZonalOcr.sln`) in Visual Studio.
3. Ensure the necessary dependencies are installed.
4. Build and run the application.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.
