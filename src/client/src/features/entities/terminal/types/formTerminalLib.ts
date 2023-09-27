import { TerminalLibAm, TerminalLibCm } from "@mimirorg/typelibrary-types";
import { ValueObject } from "features/entities/types/valueObject";

/**
 * This type functions as a layer between client needs and the backend model.
 * It allows you to adapt the expected api model to fit client/form logic needs.
 */
export interface FormTerminalLib extends Omit<TerminalLibAm, "attributes" | "attributeGroups"> {
  attributes: ValueObject<string>[];
  attributeGroups: ValueObject<string>[];
}

/**
 * Maps the client-only model back to the model expected by the backend api
 * @param formTerminal client-only model
 */
export const mapFormTerminalLibToApiModel = (formTerminal: FormTerminalLib): TerminalLibAm => ({
  ...formTerminal,
  attributes: formTerminal.attributes.map((x) => x.value),
  attributeGroups: formTerminal.attributeGroups.map((x) => x.value),
});

export const mapTerminalLibCmToFormTerminalLib = (terminalLibCm: TerminalLibCm): FormTerminalLib => ({
  ...terminalLibCm,
  attributes: terminalLibCm.attributes.map((x) => ({ value: x.id })),
  attributeGroups: terminalLibCm.attributeGroups ? terminalLibCm.attributeGroups.map((x) => ({ value: x })) : [],
});

export const createEmptyFormTerminalLib = (): FormTerminalLib => ({
  ...emptyTerminalLibAm,
  attributes: [],
  attributeGroups: [],
  color: "#d0d0dd",
});

const emptyTerminalLibAm: TerminalLibAm = {
  name: "",
  typeReference: "",
  color: "",
  description: "",
  attributes: [],
  attributeGroups: [],
};
