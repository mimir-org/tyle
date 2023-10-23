import { ReactNode } from "react";
import { Direction } from "./terminals/direction";

/**
 * Interface used to describe generic items. Descriptors are usually shorts key value pairs uniquely describing the item in question.
 *
 * @example
 * const objectWithIndexedDescriptorKeys: InfoItem = {
 *   name: "References",
 *   descriptors: {
 *     0: "PCA",
 *     1: "AAA",
 *     2: "BBB",
 *   },
 * }
 *
 * const objectWithNamedDescriptorKeys: InfoItem = {
 *   name: "Power capacity",
 *   descriptors: {
 *     condition: "Maximum",
 *     qualifier: "Rating",
 *     source: "Required",
 *   },
 * }
 */
export interface InfoItem {
  id?: string;
  name: string;
  descriptors?: { [key: string]: string | ReactNode };
  qualifier?: Direction;
}
