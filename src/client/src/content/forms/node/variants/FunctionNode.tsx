import { Control, UseFormRegister } from "react-hook-form";
import { FormNodeLib } from "../../types/formNodeLib";
import { NodeFormAttributes } from "../attributes/NodeFormAttributes";
import { NodeFormTerminals } from "../terminals/NodeFormTerminals";
import { Aspect } from "../../../../models/tyle/enums/aspect";
import { NodeFormPredefinedAttributes } from "../predefined-attributes/NodeFormPredefinedAttributes";

export interface FunctionNodeProps {
  control: Control<FormNodeLib>;
  register: UseFormRegister<FormNodeLib>;
}

export const FunctionNode = ({ control, register }: FunctionNodeProps) => {
  return (
    <>
      <NodeFormTerminals control={control} />
      <NodeFormAttributes control={control} aspects={[Aspect.Function]} />
    </>
  );
};
