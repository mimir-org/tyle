/**
 * Interface used to describe generic items.
 * Descriptors are usually shorts key value pairs uniquely describing the item in question.
 */
export interface InfoItem {
  id?: string;
  name: string;
  descriptors: { [key: string]: string };
}
