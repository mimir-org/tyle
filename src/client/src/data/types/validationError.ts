export interface ValidationError<T> {
  message: string;
  errors?: Record<keyof T, string[]>;
}
