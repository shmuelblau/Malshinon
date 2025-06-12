# Malshinon API

This project is a C# ASP.NET Core Web API designed to manage reports and intelligence on individuals.

## 📦 Endpoints

All endpoints are `POST` and expect a `RequestDTO` in the request body.

---

### 🔹 `POST /AllPeople`
Returns a list of all people in the system.

**Request Body:** `RequestDTO`  
**Response:** `ApiResponse<List<People>>`

---

### 🔹 `POST /AllReports`
Returns a list of all intelligence reports.

**Request Body:** `RequestDTO`  
**Response:** `ApiResponse<List<IntelReport>>`

---

### 🔹 `POST /AddReport`
Adds a new intelligence report.

**Request Body:** `RequestDTO`  
**Response:** `ApiResponse<string>` (Confirmation message)

---

### 🔹 `POST /Dangerous`
Returns a list of dangerous individuals based on reports.

**Request Body:** `RequestDTO`  
**Response:** `ApiResponse<List<People>>`

---

### 🔹 `POST /PotentialAgents`
Returns a list of people who are potential agents.

**Request Body:** `RequestDTO`  
**Response:** `ApiResponse<List<People>>`

---

## 🛠 Technologies

- ASP.NET Core
- Entity Framework Core (assumed)
- C#
- RESTful API design

## 🗃 Models (assumed)

- `People`
- `IntelReport`
- `RequestDTO`
- `ApiResponse<T>`

## 📋 Logging

Each endpoint logs the request using `Log.request("ActionName")`.

## 🧪 Running the project

Use `dotnet run` or Docker if using containerized environment.

---

**Author:** Malshinon Team  
**Status:** Development