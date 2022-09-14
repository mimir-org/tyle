import { InterfaceLibAm, InterfaceLibCm } from "@mimirorg/typelibrary-types";
import { UpdateEntity } from "../../../../data/types/updateEntity";
import { createEmptyInterfaceLibAm } from "../../../../models/tyle/application/interfaceLibAm";
import { mapInterfaceLibCmToInterfaceLibAm } from "../../../../utils/mappers";
import { ValueObject } from "../../types/valueObject";

/**
 * This type functions as a layer between client needs and the backend model.
 * It allows you to adapt the expected api model to fit client/form logic needs.
 */
export interface FormInterfaceLib extends Omit<UpdateEntity<InterfaceLibAm>, "attributeIdList"> {
  attributeIdList: ValueObject<string>[];
  terminalColor?: string;
}

/**
 * Maps the client-only model back to the model expected by the backend api
 * @param formInterface client-only model
 */
export const mapFormInterfaceLibToApiModel = (formInterface: FormInterfaceLib): UpdateEntity<InterfaceLibAm> => ({
  ...formInterface,
  attributeIdList: formInterface.attributeIdList.map((x) => x.value),
});

export const createEmptyFormInterfaceLib = (): FormInterfaceLib => ({
  ...createEmptyInterfaceLibAm(),
  id: "",
  attributeIdList: [],
});

export const mapInterfaceLibCmToFormInterfaceLib = (interfaceLibCm: InterfaceLibCm): FormInterfaceLib => ({
  ...mapInterfaceLibCmToInterfaceLibAm(interfaceLibCm),
  id: interfaceLibCm.id,
  attributeIdList: interfaceLibCm.attributes.map((x) => ({
    value: x.id,
  })),
  terminalColor: interfaceLibCm.terminal.color,
});
