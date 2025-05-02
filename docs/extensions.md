# PostgreSQL Extensions Documentation

This document provides an overview of the PostgreSQL extensions currently installed in the database.

## Installed Extensions

### 1. `pg_stat_statements`

- **Description**: Tracks execution statistics of all SQL statements executed by a server.
- **Use Case**: Useful for performance monitoring and query analysis. Helps identify slow or frequently executed queries.
- **Documentation**: [pg_stat_statements - PostgreSQL Official Docs](https://www.postgresql.org/docs/current/pgstatstatements.html)

---

### 2. `pgcrypto`

- **Description**: Provides cryptographic functions for PostgreSQL.
- **Use Case**: Enables encryption and decryption of data using various algorithms (e.g., AES, RSA). Also supports digest functions like SHA and MD5.
- **Documentation**: [pgcrypto - PostgreSQL Official Docs](https://www.postgresql.org/docs/current/pgcrypto.html)

---

### 3. `uuid-ossp`

- **Description**: Provides functions to generate universally unique identifiers (UUIDs).
- **Use Case**: Commonly used for generating unique keys for rows, especially in distributed systems or RESTful APIs.
- **Documentation**: [uuid-ossp - PostgreSQL Official Docs](https://www.postgresql.org/docs/current/uuid-ossp.html)
