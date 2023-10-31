export interface MimirorgUserAmCorrectTypes {
  email: string;
  password: string;
  confirmPassword: string;
  firstName: string;
  lastName: string;
  companyId?: number | null;
  purpose?: string | null;
}
