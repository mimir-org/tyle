import { ConnectorDirection, AspectObjectTerminalLibAm, AspectObjectTerminalLibCm } from "@mimirorg/typelibrary-types";
import {
  MAXIMUM_TERMINAL_QUANTITY_VALUE,
  MINIMUM_TERMINAL_QUANTITY_VALUE,
} from "common/utils/aspectObjectTerminalQuantityRestrictions";

/**
 * This type functions as a layer between client needs and the backend model.
 * It allows you to adapt the expected api model to fit client/form logic needs.
 *
 * hasMaxQuantity - used to allow a more friendly way for the user toggle between an infinite and a concrete amount of terminals.
 */
export interface FormAspectObjectTerminalLib extends AspectObjectTerminalLibAm {
  hasMaxQuantity: boolean;
}

export const mapAspectObjectTerminalLibCmToClientModel = (
  aspectObjectTerminalLibCm: AspectObjectTerminalLibCm,
): FormAspectObjectTerminalLib => ({
  ...mapAspectObjectTerminalLibCmToAspectObjectTerminalLibAm(aspectObjectTerminalLibCm),
  hasMaxQuantity:
    aspectObjectTerminalLibCm.maxQuantity > MINIMUM_TERMINAL_QUANTITY_VALUE &&
    aspectObjectTerminalLibCm.maxQuantity < MAXIMUM_TERMINAL_QUANTITY_VALUE,
});

const mapAspectObjectTerminalLibCmToAspectObjectTerminalLibAm = (
  terminal: AspectObjectTerminalLibCm,
): AspectObjectTerminalLibAm => ({
  ...terminal,
  terminalId: terminal.terminal.id,
});

export const createEmptyFormAspectObjectTerminalLib = (): FormAspectObjectTerminalLib => ({
  ...emptyAspectObjectTerminalLibAm,
  hasMaxQuantity: false,
});

const emptyAspectObjectTerminalLibAm: AspectObjectTerminalLibAm = {
  terminalId: "",
  minQuantity: 0,
  maxQuantity: 0,
  connectorDirection: ConnectorDirection.Input,
};
