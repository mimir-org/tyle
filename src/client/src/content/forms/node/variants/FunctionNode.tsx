import { Control } from "react-hook-form";
import { FormNodeLib } from "../../types/formNodeLib";
import { NodeFormAttributes } from "../NodeFormAttributes";
import { NodeFormTerminals } from "../NodeFormTerminals";
import { Aspect } from "../../../../models/tyle/enums/aspect";

export interface FunctionNodeProps {
  control: Control<FormNodeLib>;
}

export const FunctionNode = ({ control }: FunctionNodeProps) => {
  return (
    <>
      <NodeFormTerminals control={control} />
      <NodeFormAttributes control={control} aspects={[Aspect.Function]} />
    </>
  );
};
