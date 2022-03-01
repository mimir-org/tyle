export interface ValidationState<T> {
  message: string;
  errors?: Record<keyof T, string[]>;
}
