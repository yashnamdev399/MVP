export interface Car {
  id?: string;
  make: string;
  model: string;
  price: number;
  fuelType: string;
  bodyType: string;
  mileage: number;
  safetyRating: number;
  seatingCapacity: number;
  primaryAttributes: string[];
}

export interface UserPreference {
  maxBudget: number;
  minBudget: number;
  minSeats: number;
  primaryUse: string;
  topPriority: string;
}

export interface RecommendationResult {
  car: Car;
  matchScore: number;
  matchReason: string;
}
