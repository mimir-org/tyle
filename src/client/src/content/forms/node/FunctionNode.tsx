import { Control } from "react-hook-form";
import { FormNodeLib } from "../types/formNodeLib";
import { NodeFormTerminals } from "./NodeFormTerminals";

export interface FormNodeFunctionProps {
  control: Control<FormNodeLib>;
}

export const FunctionNode = ({ control }: FormNodeFunctionProps) => {
  return (
    <>
      <NodeFormTerminals control={control} />
    </>
  );
};
