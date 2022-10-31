import { AnySchema } from "yup";

/**
 * Helper for strongly typing yup schemas
 *
 * @example
 * export const mySchema = () => {
 *   return yup.object<YupShape<MyTypeWithNameProperty>>({
 *     name: yup.string().required(), // This property exists on the model and is legal
 *     illegalProperty: yup.string(), // This property does not exist on the model and would throw an error
 *   });
 * };
 */
export type YupShape<T> = Partial<Record<keyof T, AnySchema>>;
