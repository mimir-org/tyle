export interface ChangePasswordRequest {
  email: string;
  password: string;
  confirmPassword: string;
  code: string;
}
