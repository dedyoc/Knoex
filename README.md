# Knowledge Expedition (Knoex)

Knoex is a community-driven Q&A platform designed to facilitate knowledge sharing and collaboration.  Inspired by platforms like Stack Overflow, Knoex provides a structured environment for users to ask questions, provide answers, and engage in discussions on a wide range of topics. Additional features will be added later.

## Features

* **Ask Questions:**  Post clear, concise questions with relevant tags to attract knowledgeable users.
* **Provide Answers:** Share your expertise by answering questions and contributing to the knowledge base.
* **Comment and Discuss:** Engage in discussions, clarify doubts, and provide feedback through comments.
* **Vote and Rank:**  Upvote insightful questions and answers to help surface the most valuable content.
* **Accept Answers:**  Question askers can mark the most helpful answer as accepted, signaling a resolution.
* **Tagging System:**  Categorize questions with tags to improve searchability and organization.
* **User Profiles:** Showcase your contributions and expertise with a personalized profile.
* **Full-text Search:** Quickly find relevant information using powerful search capabilities.
* **Markdown Support:**  Format your questions and answers using Markdown for enhanced readability.
* **Code Syntax Highlighting:**  Share code snippets with proper syntax highlighting for clarity.
* **Related Questions:** Discover similar questions to avoid duplicates and broaden your understanding.

## Technologies Used

* **ASP.NET Core MVC:**  Provides the robust framework for building the web application.
* **Entity Framework Core:**  ORM for seamless interaction with the PostgreSQL database.
* **Identity Framework:** Secure user authentication and authorization.
* **SimpleMDE (Markdown Editor):**  Markdown editor for creating rich text content.
* **Alpine.js:**  Enables behaviours within HTML.
* **PostgreSQL with Full-text Search (tsvector):**  Efficient and scalable search functionality.
* **Docker:**  Containerization for easy deployment and environment consistency.


## Getting Started

### Prerequisites

* Docker and Docker Compose (and .NET 6 to recreate the db from host machine)

### Running the Application

1. Clone the repository:

   ```bash
   git clone https://github.com/dedyoc/Knoex.git
   ```

2. Navigate to the project directory:

   ```bash
   cd Knoex/Knoex
   ```

3. Start the database using Docker Compose then recreate the db schema:

   ```bash
   docker compose up -d
   dotnet ef database update
   ```
4. Start the app with `dotnet run`.
5. Access the application in your browser at `http://localhost:5142` or `http://localhost:7017`.

### Development Setup
In Progress
<!-- 
For development, you can use the development version of the image and mount the source code:

1. Build the development image:

    ```bash
   make image/dev
    ```

2.  Start the development environment:

    ```bash
    make docker/develop
    ```
3. The application will be accessible at `http://localhost:5000`.  Code changes will be automatically reflected. -->

## Running Tests
In progress
<!-- * **Unit Tests:** `make test/unit`
* **Integration Tests:** `make test/integration` -->

## Contributing

Contributions are welcome! Please feel free to open issues and submit pull requests.

## License

This project is licensed under the MIT License.
