import { AttributeLibAm, InterfaceLibAm, InterfaceLibCm } from "@mimirorg/typelibrary-types";
import { UpdateEntity } from "../../../../data/types/updateEntity";
import { createEmptyInterfaceLibAm } from "../../../../models/tyle/application/interfaceLibAm";
import { ValueObject } from "../../types/valueObject";
import { InterfaceFormMode } from "./interfaceFormMode";

/**
 * This type functions as a layer between client needs and the backend model.
 * It allows you to adapt the expected api model to fit client/form logic needs.
 */
export interface FormInterfaceLib extends Omit<InterfaceLibAm, "attributes"> {
  attributes: ValueObject<UpdateEntity<AttributeLibAm>>[];
  terminalColor?: string;
}

/**
 * Maps the client-only model back to the model expected by the backend api
 * @param formInterface client-only model
 */
export const mapFormInterfaceLibToApiModel = (formInterface: FormInterfaceLib): InterfaceLibAm => ({
  ...formInterface,
  attributes: formInterface.attributes.map((x) => x.value),
});

export const createEmptyFormInterfaceLib = (): FormInterfaceLib => ({
  ...createEmptyInterfaceLibAm(),
  attributes: [],
});

export const mapInterfaceLibCmToFormInterfaceLib = (
  interfaceLibCm: InterfaceLibCm,
  mode?: InterfaceFormMode
): FormInterfaceLib => ({
  ...interfaceLibCm,
  parentId: mode === "clone" ? interfaceLibCm.id : interfaceLibCm.parentId,
  attributes: interfaceLibCm.attributes.map((x) => ({ value: x })),
  terminalColor: interfaceLibCm.terminal.color,
});
