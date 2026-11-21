# Kohonen Self-Organizing Map (SOM) Simulator

An interactive desktop application developed in **C# using .NET 8 and Windows Forms**, designed to demonstrate the learning process of a **Kohonen Self-Organizing Map (SOM)**.

The project was developed as part of a university neural networks course. Its primary goal is to visualize unsupervised learning and investigate how different hyperparameters affect the network's performance.

Language note: The application's user interface is currently available in Polish. This README provides documentation in English to make the project accessible to a wider audience.
## Features

### Interactive SOM Simulation

- Generation of 300 synthetic 2D data points distributed across three clusters.
- Real-time visualization of neurons and their connections within the SOM grid.
- Visualization of neuron movement and self-organization during training.
- Configurable map dimensions, number of training epochs, and initial learning rate.
- Training controls: **Start, Stop, Resume, and Reset**.
- Training progress tracking with an epoch counter and progress bar.
- Real-time visualization of quantization error throughout training.

### Experimental Module

The application includes an experimental module for evaluating the impact of selected hyperparameters on SOM performance.

Three parameters can be investigated independently:

| Parameter | Tested Values |
|---|---|
| Map size | 4×4, 8×8, 12×12 |
| Training epochs | 10, 50, 100, 500 |
| Initial learning rate | 0.05, 0.10, 0.30, 0.50 |

Each experiment changes one parameter while keeping the others fixed.

The application measures:

- **Average Quantization Error (QE)** – the average distance between input samples and their Best Matching Units.
- **Training time** – the time required to complete training, measured in milliseconds.

Each configuration is evaluated across five training runs, with the results averaged for comparison.

Additional features include exporting experimental results to **CSV** and saving comparison charts as **PNG** images.

## How It Works

A Self-Organizing Map is an artificial neural network that uses **unsupervised learning** to organize input data according to similarities.

The network consists of neurons arranged in a grid, with each neuron represented by a weight vector.

For each input sample, the training algorithm performs three main steps:

1. **Competition** – identifies the Best Matching Unit (BMU), the neuron whose weight vector has the smallest Euclidean distance to the input sample.
2. **Cooperation** – determines the influence of the winning neuron on other neurons using a Gaussian neighborhood function.
3. **Adaptation** – updates the weight vectors of the winning neuron and its neighbors toward the input sample.

The learning rate and neighborhood radius gradually decrease during training, allowing the network to transition from large-scale organization to more localized adjustments.

The final SOM grid provides a structured representation of the input data while aiming to preserve topological relationships between neurons.

## Screenshots

### SOM Simulation

<img width="1353" height="937" alt="image" src="https://github.com/user-attachments/assets/7ed9b488-cc3c-4850-9cd1-7dacd82448c3" />

### SOM After Training

<img width="1350" height="931" alt="image" src="https://github.com/user-attachments/assets/4d15f3a9-7d91-40e9-92a0-fbb8fbee5d71" />

### Experimental Results

<img width="1358" height="932" alt="image" src="https://github.com/user-attachments/assets/05cdb1f5-09f5-4247-97ef-dda1f3da3a81" />


## Experiments and Results

The application was evaluated using a synthetic dataset containing 300 two-dimensional observations distributed across three clusters.

The baseline configuration consisted of an 8×8 SOM grid, 100 training epochs, and an initial learning rate of 0.30.

### Experiment 1: Map Size

| Map Size | Average Quantization Error |
|---|---:|
| 4×4 | 0.0473 |
| 8×8 | 0.0212 |
| 12×12 | 0.0123 |

Increasing the map size reduced quantization error in the tested configurations. However, larger maps also require more computational operations.

### Experiment 2: Number of Training Epochs

| Epochs | Average Quantization Error |
|---|---:|
| 10 | 0.0249 |
| 50 | 0.0215 |
| 100 | 0.0210 |
| 500 | 0.0210 |

Most improvements occurred during the early stages of training. Increasing the number of epochs beyond 100 did not produce a noticeable improvement at the reported precision.

### Experiment 3: Initial Learning Rate

| Initial Learning Rate | Average Quantization Error |
|---|---:|
| 0.05 | 0.0236 |
| 0.10 | 0.0223 |
| 0.30 | 0.0215 |
| 0.50 | 0.0209 |

Higher initial learning rates resulted in slightly lower quantization errors within the tested range. The best result was obtained with a learning rate of 0.50.

**Note:** These results are specific to the generated dataset and tested configurations. They should not be interpreted as universally optimal SOM parameters.

## Technologies Used

- **C#** – application logic and SOM implementation.
- **.NET 8** – application framework.
- **Windows Forms (WinForms)** – desktop graphical user interface.
- **Visual Studio** – development environment.

## Getting Started

### Prerequisites

- Windows 10 or Windows 11
- Visual Studio 2022 with the .NET desktop development workload
- .NET 8 SDK

### Installation

1. Clone the repository:

   `git clone https://github.com/YOUR_USERNAME/KohonenSOM.git`

2. Open `KohonenSOM.sln` in Visual Studio.
3. Restore the required dependencies.
4. Build the solution.
5. Run the application.

Alternatively, download the repository as a ZIP archive and open the solution file in Visual Studio.

## Project Structure

The application is organized into components responsible for the user interface, SOM training algorithm, data generation, and experimental evaluation.

The core SOM implementation includes:

- Identifying the Best Matching Unit using Euclidean distance.
- Updating neuron weights using a Gaussian neighborhood function.
- Calculating quantization error.
- Managing the training process across multiple epochs.

## Future Improvements

Potential extensions include:

- Importing custom datasets from CSV files.
- Supporting multidimensional input data.
- Allowing users to configure the number and distribution of generated observations.
- Adding additional evaluation metrics and visualization options.
- Comparing SOM performance across different neighborhood functions.

## Author

**Patryk Sitek**

Academic project developed for a Neural Networks course.
