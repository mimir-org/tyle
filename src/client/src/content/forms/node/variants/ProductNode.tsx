import { Control } from "react-hook-form";
import { NodeFormTerminalTable } from "../terminals/NodeFormTerminalTable";
import { FormNodeLib } from "../types/formNodeLib";

export interface ProductNodeProps {
  control: Control<FormNodeLib>;
}

export const ProductNode = ({ control }: ProductNodeProps) => (
  <>
    <NodeFormTerminalTable control={control} />
  </>
);
