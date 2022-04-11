/**
 * Type for extending a given type <T> with an id property.
 * @example
 * function updateMyModel(item: UpdateEntity<ModelWithoutAnID>){
 *   return someCall(item.id);
 * }
 */
export type UpdateEntity<T> = T & {
  id: string;
};
