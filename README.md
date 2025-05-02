# Wallety Portal Api

## Overview

This document describes the Validity Portal API, a C# implementation built using Clean Architecture principles. The API provides services for [brief description of what the API does].

## Clean Architecture Layers

### 1. Domain Layer

**Responsibilities**: Contains enterprise-wide business logic and entities.

Core Entities:

- `MilitaryPortal` - Main aggregate root

- `Post` - Represents content posts

- `Terminal` - System access point entity

### 2. Application Layer

**Responsibilities**: Contains use cases and business rules.

Key Use Cases:

- `MilitaryPortalService` - Handles core portal operations

- `PostManagementService` - Manages content posts

- `TerminalAccessService` - Controls terminal access

Interfaces:

```csharp
public interface IMilitaryPortalRepository
{
    Task<MilitaryPortal> GetByIdAsync(int id);
    Task AddAsync(MilitaryPortal portal);
    // Other repository methods
}
```

### 3. Infrastructure Layer

**Responsibilities**: Implements interfaces defined in Application layer.

Implementations:

- `MilitaryPortalRepository` - EF Core implementation

- `EmailService` - SMTP implementation

- `FileStorageService` - Azure Blob Storage implementation

### 4. Presentation Layer

**Responsibilities**: Contains API controllers and DTOs.

Controllers:

```csharp
[ApiController]
[Route("api/[controller]")]
public class MilitaryPortalController : ControllerBase
{
    private readonly IMilitaryPortalService _portalService;

    public MilitaryPortalController(IMilitaryPortalService portalService)
    {
        _portalService = portalService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MilitaryPortalDto>> GetById(int id)
    {
        var portal = await _portalService.GetByIdAsync(id);
        return Ok(portal.ToDto());
    }
}
```
