import { State } from "@mimirorg/typelibrary-types";

/**
   * Creates an instance of FormAttributeHelper.
      
   * @name {string} Name of attribute.
 * @state {State} State of the attribute.
 * * @description {string} Description of attribute.
 * * @symbol {string} Symbol of the attribute.
 * * @unitId {string} unit id of the attribute. 
   */
export interface FormAttributeHelper {
  name: string;
  state: State;
  description: string;
  symbol: string;
  unitId: string;
}
