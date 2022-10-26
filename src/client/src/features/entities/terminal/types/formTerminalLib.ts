import { AttributeLibAm, TerminalLibAm, TerminalLibCm } from "@mimirorg/typelibrary-types";
import { UpdateEntity } from "../../../../common/types/updateEntity";
import { ValueObject } from "../../types/valueObject";
import { TerminalFormMode } from "./terminalFormMode";

/**
 * This type functions as a layer between client needs and the backend model.
 * It allows you to adapt the expected api model to fit client/form logic needs.
 */
export interface FormTerminalLib extends Omit<TerminalLibAm, "attributes"> {
  attributes: ValueObject<UpdateEntity<AttributeLibAm>>[];
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
  attributes: terminalLibCm.attributes.map((x) => ({ value: x })),
});

export const createEmptyFormTerminalLib = (): FormTerminalLib => ({
  ...emptyTerminalLibAm,
  attributes: [],
  color: "#f7f6ff",
});

const emptyTerminalLibAm: TerminalLibAm = {
  name: "",
  parentId: "",
  typeReferences: [],
  color: "",
  description: "",
  attributes: [],
  companyId: 0,
  version: "1.0",
};
