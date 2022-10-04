import { TerminalLibAm, TerminalLibCm } from "@mimirorg/typelibrary-types";
import { createEmptyTerminalLibAm } from "../../../../models/tyle/application/terminalLibAm";
import { mapTerminalLibCmToTerminalLibAm } from "../../../../utils/mappers";
import { ValueObject } from "../../types/valueObject";
import { TerminalFormMode } from "./terminalFormMode";

/**
 * This type functions as a layer between client needs and the backend model.
 * It allows you to adapt the expected api model to fit client/form logic needs.
 */
export interface FormTerminalLib extends Omit<TerminalLibAm, "attributeIdList"> {
  attributeIdList: ValueObject<string>[];
}

/**
 * Maps the client-only model back to the model expected by the backend api
 * @param formTerminal client-only model
 */
export const mapFormTerminalLibToApiModel = (formTerminal: FormTerminalLib): TerminalLibAm => ({
  ...formTerminal,
  attributeIdList: formTerminal.attributeIdList.map((x) => x.value),
});

export const createEmptyFormTerminalLib = (): FormTerminalLib => ({
  ...createEmptyTerminalLibAm(),
  attributeIdList: [],
  color: "#f7f6ff",
});

export const mapTerminalLibCmToFormTerminalLib = (
  terminalLibCm: TerminalLibCm,
  mode?: TerminalFormMode
): FormTerminalLib => ({
  ...mapTerminalLibCmToTerminalLibAm(terminalLibCm),
  parentId: mode === "clone" ? terminalLibCm.id : terminalLibCm.parentId,
  attributeIdList: terminalLibCm.attributes.map((x) => ({
    value: x.id,
  })),
});
