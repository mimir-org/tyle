import { AttributeLibAm, TerminalLibAm, TerminalLibCm } from "@mimirorg/typelibrary-types";
import { UpdateEntity } from "common/types/updateEntity";
import { TerminalFormMode } from "features/entities/terminal/types/terminalFormMode";
import { ValueObject } from "features/entities/types/valueObject";

/**
 * This type functions as a layer between client needs and the backend model.
 * It allows you to adapt the expected api model to fit client/form logic needs.
 */
export interface FormTerminalLib extends Omit<TerminalLibAm, "attributes"> {
  attributes: ValueObject<string>[];
}

/**
 * Maps the client-only model back to the model expected by the backend api
 * @param formTerminal client-only model
 */
export const mapFormTerminalLibToApiModel = (formTerminal: FormTerminalLib): TerminalLibAm => ({
  ...formTerminal,
  attributes: formTerminal.attributes.map((x) => x.value),
});

export const mapTerminalLibCmToFormTerminalLib = (
  terminalLibCm: TerminalLibCm,
  mode?: TerminalFormMode
): FormTerminalLib => ({
  ...terminalLibCm,
  parentId: mode === "clone" ? terminalLibCm.id : terminalLibCm.parentId,
  attributes: terminalLibCm.attributes.map((x) => ({ value: x.id })),
});

export const createEmptyFormTerminalLib = (): FormTerminalLib => ({
  ...emptyTerminalLibAm,
  attributes: [],
  color: "#f7f6ff",
});

const emptyTerminalLibAm: TerminalLibAm = {
  name: "",
  parentId: "",
  typeReference: "",
  color: "",
  description: "",
  attributes: [],
  companyId: 0,
};
