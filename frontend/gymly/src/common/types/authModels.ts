import { Role } from '@serviceTypes/constants';

export interface LoginRequest {
  email: string;
  password: string;
}

export interface LoginResponse {
  token: string;
}

export interface User {
  id: number;
  name: string;
  email: string;
  phone: string;
  role: Role | null;
}
