import { TerminalLibAm, TerminalLibCm } from "@mimirorg/typelibrary-types";
import { UpdateEntity } from "../../../../data/types/updateEntity";
import { createEmptyTerminalLibAm } from "../../../../models/tyle/application/terminalLibAm";
import { mapTerminalLibCmToTerminalLibAm } from "../../../../utils/mappers/mapTerminalLibCmToTerminalLibAm";
import { ValueObject } from "../../types/valueObject";

/**
 * This type functions as a layer between client needs and the backend model.
 * It allows you to adapt the expected api model to fit client/form logic needs.
 */
export interface FormTerminalLib extends Omit<UpdateEntity<TerminalLibAm>, "attributeIdList"> {
  attributeIdList: ValueObject<string>[];
}

/**
 * Maps the client-only model back to the model expected by the backend api
 * @param formTerminal client-only model
 */
export const mapFormTerminalLibToApiModel = (formTerminal: FormTerminalLib): UpdateEntity<TerminalLibAm> => ({
  ...formTerminal,
  attributeIdList: formTerminal.attributeIdList.map((x) => x.value),
});

export const createEmptyFormTerminalLib = (): FormTerminalLib => ({
  ...createEmptyTerminalLibAm(),
  id: "",
  attributeIdList: [],
});

export const mapTerminalLibCmToFormTerminalLib = (terminalLibCm: TerminalLibCm): FormTerminalLib => ({
  ...mapTerminalLibCmToTerminalLibAm(terminalLibCm),
  id: terminalLibCm.id,
  attributeIdList: terminalLibCm.attributes.map((x) => ({
    value: x.id,
  })),
});
