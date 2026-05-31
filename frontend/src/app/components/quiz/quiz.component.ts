import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CarService } from '../../services/car.service';
import { UserPreference, RecommendationResult } from '../../models/car.model';

@Component({
  selector: 'app-quiz',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './quiz.component.html',
  styleUrl: './quiz.component.css'
})
export class QuizComponent {
  private carService = inject(CarService);

  currentStep = 1;
  totalSteps = 4;
  loading = false;
  results: RecommendationResult[] = [];

  // Step 1: Budget
  budgetLakhs = 10;
  budgetPresets = [6, 8, 10, 12, 15, 20];

  // Step 2: Seating
  selectedSeats = 5;
  seatOptions = [5, 6, 7];

  // Step 3: Lifestyle
  selectedLifestyle = '';
  lifestyleOptions = [
    { label: 'City Commuting', value: 'City Commuting', icon: '🏙️' },
    { label: 'Highway Cruising', value: 'Highway Cruising', icon: '🛣️' },
    { label: 'Rough Roads', value: 'Rough Roads', icon: '⛰️' },
    { label: 'Family Car', value: 'Family Car', icon: '👨‍👩‍👧‍👦' }
  ];

  // Step 4: Dealbreaker
  selectedPriority = '';
  priorityOptions = [
    { label: 'Top Safety Rating', value: 'Safety', icon: '🛡️' },
    { label: 'Best Mileage', value: 'FuelEconomy', icon: '⛽' }
  ];

  setBudget(lakhs: number) {
    this.budgetLakhs = lakhs;
  }

  setSeats(seats: number) {
    this.selectedSeats = seats;
  }

  setLifestyle(value: string) {
    this.selectedLifestyle = value;
  }

  setPriority(value: string) {
    this.selectedPriority = value;
  }

  nextStep() {
    if (this.currentStep < this.totalSteps) {
      this.currentStep++;
    }
  }

  prevStep() {
    if (this.currentStep > 1) {
      this.currentStep--;
    }
  }

  canProceed(): boolean {
    switch (this.currentStep) {
      case 1: return this.budgetLakhs > 0;
      case 2: return this.selectedSeats > 0;
      case 3: return this.selectedLifestyle !== '';
      case 4: return this.selectedPriority !== '';
      default: return false;
    }
  }

  submit() {
    const prefs: UserPreference = {
      maxBudget: this.budgetLakhs * 100000,
      minBudget: 0,
      minSeats: this.selectedSeats,
      primaryUse: this.selectedLifestyle,
      topPriority: this.selectedPriority
    };

    this.loading = true;
    this.carService.getRecommendations(prefs).subscribe({
      next: (data) => {
        this.results = data;
        this.loading = false;
        this.currentStep = 5;
      },
      error: () => {
        this.loading = false;
        this.results = [];
        this.currentStep = 5;
      }
    });
  }

  restart() {
    this.currentStep = 1;
    this.results = [];
    this.budgetLakhs = 10;
    this.selectedSeats = 5;
    this.selectedLifestyle = '';
    this.selectedPriority = '';
  }

  formatPrice(price: number): string {
    return '₹' + (price / 100000).toFixed(1) + ' Lakh';
  }

  getStars(rating: number): string {
    return '★'.repeat(rating) + '☆'.repeat(5 - rating);
  }

  getScoreClass(score: number): string {
    if (score >= 90) return 'excellent';  // green
    if (score >= 70) return 'good';       // blue
    if (score >= 60) return 'fair';       // amber/orange
    return 'low';                         // grey
  }
}
