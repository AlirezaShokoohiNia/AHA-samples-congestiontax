# Business Requirements – Congestion Tax System

---

### 1. Background
Cities such as Gothenburg impose congestion taxes to reduce traffic and pollution.  
The municipality requires a reliable system to calculate and enforce these fees for vehicles entering or leaving the city during specified hours.

### 2. Objectives
- Record vehicle passes with timestamps and city context.  
- Calculate congestion tax fees according to city‑specific rules.  
- Enforce exemptions and daily caps.  
- Support multiple cities with different rule sets, managed externally.

### 3. Core Business Rules (Gothenburg Example)

#### 3.1 Time‑Based Fees
- Fixed hourly intervals with associated fees (SEK).  
- Example: 06:00–06:29 → 8 SEK, 07:00–07:59 → 18 SEK.

#### 3.2 Daily Cap
- Maximum fee per vehicle per day: **60 SEK**.

#### 3.3 Exemptions
- No tax on weekends (Saturday, Sunday).  
- No tax on public holidays or the day before a public holiday.  
- No tax during July.  
- Toll‑free vehicle categories: Emergency, Bus, Diplomat, Motorcycle, Military, Foreign.

#### 3.4 Single Charge Rule
- If a vehicle passes multiple tolling stations within 60 minutes, only the highest fee applies.

### 4. Requirements

#### 4.1 Functional
- Record vehicle passes with license plate, city, and timestamp.  
- Retrieve applicable rule set for the city at runtime.  
- Calculate daily fee per vehicle, applying exemptions and caps.  
- Persist results for auditing and reporting.

#### 4.2 Non‑Functional
- **Correctness:** Must strictly follow city rules.  
- **Extensibility:** Support additional cities with different rules.  
- **Maintainability:** Rules must be externalized (JSON, DB, Redis, API).  
- **Testability:** Business rules must be validated via automated tests.

### 5. Externalization of Rules
- Rule sets are stored outside the application (e.g., JSON files).  
- Each city has its own file: `/rules/{city}.rules.json`.  
- Content editors can update rules without redeployment.  
- Future implementations may use Redis, databases, or APIs.

### 6. Deliverables
- A clean, domain‑driven codebase implementing the above rules.  
- Automated tests covering domain, application, infrastructure, and API layers.  
- Documentation of architecture and rule set format.
