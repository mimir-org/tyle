import { ConnectorDirection } from "@mimirorg/typelibrary-types";
import { FormNodeTerminalLib } from "features/entities/node/types/formNodeLib";

export const createEmptyNodeTerminalLibAm = (): FormNodeTerminalLib => ({
  terminalId: "",
  quantity: 1,
  connectorDirection: ConnectorDirection.Input,
  hasMaxLimit: false,
});
