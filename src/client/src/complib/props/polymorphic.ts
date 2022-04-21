import { ElementType } from "react";

export interface Polymorphic<T extends ElementType> {
  as?: T;
}
