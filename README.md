# GlossyCommerce

GlossyCommerce is a modern, cross-platform e-commerce application built using a **hybrid architecture** that targets both Web and Android environments from a single codebase. Featuring a high-performance ASP.NET Core Web API backend, a robust SQL Server database tier managed via Entity Framework Core, and an immersive, custom-styled **Cyberpunk Dark Neon** interface utilizing advanced Blazor Hybrid capabilities.

---

## 🚀 Key Features

### 🛒 Customer Storefront
* **Dynamic Product Catalog:** Auto-adjusting product grids displaying active products with clean aspect-ratio formatting.
* **Persistent Cart Management:** Seamless client-side addition and removal of inventory line items.
* **Automated Checkout Pipeline:** Integrated server-side transaction logging with decoupled automated email systems.

### 📊 Administrative Dashboard
* **Secure Authorization Layer:** Customized role-based access control protecting administrative routes (`/admin`) from unauthorized sessions.
* **Real-Time Sales Tracking:** Live aggregation metrics measuring total order volume, overall revenue streams, and margin-based profit approximations.
* **Inventory Control Suite:** Complete CRUD capability allowing admins to instantly publish new catalog entities or securely retire products while preserving existing ledger relations.

### 🛡️ System Robustness & Data Integrity
* **Refined Exception Handling:** Gracefully handles database structural blocks—such as foreign key reference conflicts when attempting to delete products attached to historical orders—alerting administrators without thread termination.
* **Asynchronous Notification Delivery:** Leverages isolated SMTP pipelines configured for Google’s modern App Password authentication to simultaneously send formatted transaction receipts to customers and administrative alert blocks.

---

## 🛠️ Tech Stack & Architecture

* **Cross-Platform Core:** .NET 8.0 / .NET MAUI (Multi-platform App UI)
* **Frontend UI Engine:** Blazor Hybrid & InteractiveServer Razor Components
* **Styling Framework:** Custom CSS Glassmorphism (Dark theme with vibrant Cyan and Pink neon accents)
* **Backend Framework:** ASP.NET Core Web API (Decoupled Controller Architecture)
* **Data Layer:** Entity Framework Core (Code-First Migration Pipeline)
* **Database Engine:** Microsoft SQL Server (SSMS)
* **Communication Layer:** HttpClient / RESTful API Architecture

---

## 💻 Database Schema Design

The relational architecture ensures strict data integrity across transactions:

* **Users Table:** Manages accounts, credentials, and structural access rights (`Admin` vs. `Customer`).
* **Products Table:** Holds catalog items, prices, descriptions, and structural images.
* **Orders Table:** Tracks master sales receipts, totals, timestamps, and customer metadata.
* **OrderItems Table:** A bridge table mapping products to specific order IDs, maintaining a foreign key reference to prevent inventory data loss.

---

## 🔧 Installation & Local Setup

### 1. Prerequisites
Ensure the following development environments are installed on your machine:
* [.NET 8.0 SDK](https://dotnet.microsoft.com/download)
* [Visual Studio 2022](https://visualstudio.microsoft.com/) *(with **.NET MAUI** and **ASP.NET/Web Development** workloads enabled)*
* [SQL Server Management Studio (SSMS)](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms)

### 2. Database Initialization
1. Launch SSMS and connect to your local SQL Server instance.
2. Execute the following statement to allocate the database:
---
## Images
<img width="1920" height="1200" alt="image" src="https://github.com/user-attachments/assets/034c1bb6-842f-4a9a-a44c-9ddf24a752cf" />
<img width="1920" height="1200" alt="image" src="https://github.com/user-attachments/assets/55b8c37a-d5ea-4322-9229-e6dd661948d9" />
<img width="1920" height="1200" alt="image" src="https://github.com/user-attachments/assets/5c2d4257-c5fc-41d3-8566-f1cdbe044c08" />

   ```sql
   CREATE DATABASE ECommerceDb;
