import { Control } from "react-hook-form";
import { NodeFormTerminalTable } from "../terminals/NodeFormTerminalTable";
import { FormNodeLib } from "../types/formNodeLib";

export interface FunctionNodeProps {
  control: Control<FormNodeLib>;
}

export const FunctionNode = ({ control }: FunctionNodeProps) => (
  <>
    <NodeFormTerminalTable control={control} />
  </>
);
