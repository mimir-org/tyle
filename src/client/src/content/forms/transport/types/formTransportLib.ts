import { TransportLibAm, TransportLibCm } from "@mimirorg/typelibrary-types";
import { createEmptyTransportLibAm } from "../../../../models/tyle/application/transportLibAm";
import { mapTransportLibCmToTransportLibAm } from "../../../../utils/mappers";
import { ValueObject } from "../../types/valueObject";
import { TransportFormMode } from "./transportFormMode";

/**
 * This type functions as a layer between client needs and the backend model.
 * It allows you to adapt the expected api model to fit client/form logic needs.
 */
export interface FormTransportLib extends Omit<TransportLibAm, "attributeIdList"> {
  attributeIdList: ValueObject<string>[];
  terminalColor?: string;
}

/**
 * Maps the client-only model back to the model expected by the backend api
 * @param formTransport client-only model
 */
export const mapFormTransportLibToApiModel = (formTransport: FormTransportLib): TransportLibAm => ({
  ...formTransport,
  attributeIdList: formTransport.attributeIdList.map((x) => x.value),
});

export const createEmptyFormTransportLib = (): FormTransportLib => ({
  ...createEmptyTransportLibAm(),
  attributeIdList: [],
});

export const mapTransportLibCmToFormTransportLib = (
  transportLibCm: TransportLibCm,
  mode?: TransportFormMode
): FormTransportLib => ({
  ...mapTransportLibCmToTransportLibAm(transportLibCm),
  parentId: mode === "clone" ? transportLibCm.id : transportLibCm.parentId,
  attributeIdList: transportLibCm.attributes.map((x) => ({
    value: x.id,
  })),
  terminalColor: transportLibCm.terminal.color,
});
