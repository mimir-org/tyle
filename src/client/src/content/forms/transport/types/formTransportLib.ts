import { TransportLibAm, TransportLibCm } from "@mimirorg/typelibrary-types";
import { UpdateEntity } from "../../../../data/types/updateEntity";
import { createEmptyTransportLibAm } from "../../../../models/tyle/application/transportLibAm";
import { mapTransportLibCmToTransportLibAm } from "../../../../utils/mappers";
import { ValueObject } from "../../types/valueObject";

/**
 * This type functions as a layer between client needs and the backend model.
 * It allows you to adapt the expected api model to fit client/form logic needs.
 */
export interface FormTransportLib extends Omit<UpdateEntity<TransportLibAm>, "attributeIdList"> {
  attributeIdList: ValueObject<string>[];
  terminalColor?: string;
}

/**
 * Maps the client-only model back to the model expected by the backend api
 * @param formTransport client-only model
 */
export const mapFormTransportLibToApiModel = (formTransport: FormTransportLib): UpdateEntity<TransportLibAm> => ({
  ...formTransport,
  attributeIdList: formTransport.attributeIdList.map((x) => x.value),
});

export const createEmptyFormTransportLib = (): FormTransportLib => ({
  ...createEmptyTransportLibAm(),
  id: "",
  attributeIdList: [],
});

export const mapTransportLibCmToFormTransportLib = (transportLibCm: TransportLibCm): FormTransportLib => ({
  ...mapTransportLibCmToTransportLibAm(transportLibCm),
  id: transportLibCm.id,
  attributeIdList: transportLibCm.attributes.map((x) => ({
    value: x.id,
  })),
  terminalColor: transportLibCm.terminal.color,
});
