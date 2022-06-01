import { Control } from "react-hook-form";
import { FormNodeLib } from "../types/formNodeLib";
import { NodeFormAttributes } from "./NodeFormAttributes";
import { NodeFormTerminals } from "./NodeFormTerminals";
import { Aspect } from "../../../models/tyle/enums/aspect";

export interface FormNodeFunctionProps {
  control: Control<FormNodeLib>;
}

export const FunctionNode = ({ control }: FormNodeFunctionProps) => {
  return (
    <>
      <NodeFormTerminals control={control} />
      <NodeFormAttributes control={control} aspects={[Aspect.Function]} />
    </>
  );
};
