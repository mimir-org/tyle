import { Aspect, AttributeLibAm, TransportLibAm, TransportLibCm } from "@mimirorg/typelibrary-types";
import { UpdateEntity } from "../../../../data/types/updateEntity";
import { ValueObject } from "../../types/valueObject";
import { TransportFormMode } from "./transportFormMode";

/**
 * This type functions as a layer between client needs and the backend model.
 * It allows you to adapt the expected api model to fit client/form logic needs.
 */
export interface FormTransportLib extends Omit<TransportLibAm, "attributes"> {
  attributes: ValueObject<UpdateEntity<AttributeLibAm>>[];
  terminalColor?: string;
}

/**
 * Maps the client-only model back to the model expected by the backend api
 * @param formTransport client-only model
 */
export const mapFormTransportLibToApiModel = (formTransport: FormTransportLib): TransportLibAm => ({
  ...formTransport,
  attributes: formTransport.attributes.map((x) => x.value),
});

export const mapTransportLibCmToFormTransportLib = (
  transportLibCm: TransportLibCm,
  mode?: TransportFormMode
): FormTransportLib => ({
  ...transportLibCm,
  parentId: mode === "clone" ? transportLibCm.id : transportLibCm.parentId,
  attributes: transportLibCm.attributes.map((x) => ({ value: x })),
  terminalColor: transportLibCm.terminal.color,
});

export const createEmptyFormTransportLib = (): FormTransportLib => ({
  ...emptyTransportLibAm,
  attributes: [],
});

const emptyTransportLibAm: TransportLibAm = {
  name: "",
  rdsName: "",
  rdsCode: "",
  purposeName: "",
  aspect: Aspect.None,
  companyId: 0,
  terminalId: "",
  attributes: [],
  description: "",
  typeReferences: [],
  parentId: "",
  version: "1.0",
};
