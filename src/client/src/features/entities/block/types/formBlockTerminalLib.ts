import { ConnectorDirection, BlockTerminalLibAm, BlockTerminalLibCm } from "@mimirorg/typelibrary-types";
import {
  MAXIMUM_TERMINAL_QUANTITY_VALUE,
  MINIMUM_TERMINAL_QUANTITY_VALUE,
} from "common/utils/blockTerminalQuantityRestrictions";

/**
 * This type functions as a layer between client needs and the backend model.
 * It allows you to adapt the expected api model to fit client/form logic needs.
 *
 * hasMaxQuantity - used to allow a more friendly way for the user toggle between an infinite and a concrete amount of terminals.
 */
export interface FormBlockTerminalLib extends BlockTerminalLibAm {
  hasMaxQuantity: boolean;
}

export const mapBlockTerminalLibCmToClientModel = (blockTerminalLibCm: BlockTerminalLibCm): FormBlockTerminalLib => ({
  ...mapBlockTerminalLibCmToBlockTerminalLibAm(blockTerminalLibCm),
  hasMaxQuantity:
    blockTerminalLibCm.maxQuantity > MINIMUM_TERMINAL_QUANTITY_VALUE &&
    blockTerminalLibCm.maxQuantity < MAXIMUM_TERMINAL_QUANTITY_VALUE,
});

const mapBlockTerminalLibCmToBlockTerminalLibAm = (terminal: BlockTerminalLibCm): BlockTerminalLibAm => ({
  ...terminal,
  terminalId: terminal.terminal.id,
});

export const createEmptyFormBlockTerminalLib = (): FormBlockTerminalLib => ({
  ...emptyBlockTerminalLibAm,
  hasMaxQuantity: false,
});

const emptyBlockTerminalLibAm: BlockTerminalLibAm = {
  terminalId: "",
  minQuantity: 0,
  maxQuantity: 0,
  connectorDirection: ConnectorDirection.Input,
};
