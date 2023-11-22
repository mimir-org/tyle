/**
 * Type for extending a given type <T> with an id property.
 * @example
 * function updateMyModel(item: TypeWithId<ModelWithoutAnID>){
 *   return someCall(item.id);
 * }
 */
export type TypeWithId<T> = T & {
  id: string;
};
