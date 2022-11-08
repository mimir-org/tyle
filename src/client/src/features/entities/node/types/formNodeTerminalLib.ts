import { ConnectorDirection, NodeTerminalLibAm, NodeTerminalLibCm } from "@mimirorg/typelibrary-types";
import {
  MAXIMUM_TERMINAL_QUANTITY_VALUE,
  MINIMUM_TERMINAL_QUANTITY_VALUE,
} from "common/utils/nodeTerminalQuantityRestrictions";

/**
 * This type functions as a layer between client needs and the backend model.
 * It allows you to adapt the expected api model to fit client/form logic needs.
 *
 * hasMaxQuantity - used to allow a more friendly way for the user toggle between an infinite and a concrete amount of terminals.
 */
export interface FormNodeTerminalLib extends NodeTerminalLibAm {
  hasMaxQuantity: boolean;
}

export const mapNodeTerminalLibCmToClientModel = (nodeTerminalLibCm: NodeTerminalLibCm): FormNodeTerminalLib => ({
  ...mapNodeTerminalLibCmToNodeTerminalLibAm(nodeTerminalLibCm),
  hasMaxQuantity:
    nodeTerminalLibCm.maxQuantity > MINIMUM_TERMINAL_QUANTITY_VALUE &&
    nodeTerminalLibCm.maxQuantity < MAXIMUM_TERMINAL_QUANTITY_VALUE,
});

const mapNodeTerminalLibCmToNodeTerminalLibAm = (terminal: NodeTerminalLibCm): NodeTerminalLibAm => ({
  ...terminal,
  terminalId: terminal.terminal.id,
});

export const createEmptyFormNodeTerminalLib = (): FormNodeTerminalLib => ({
  ...emptyNodeTerminalLibAm,
  hasMaxQuantity: false,
});

const emptyNodeTerminalLibAm: NodeTerminalLibAm = {
  terminalId: "",
  minQuantity: 0,
  maxQuantity: 0,
  connectorDirection: ConnectorDirection.Input,
};
