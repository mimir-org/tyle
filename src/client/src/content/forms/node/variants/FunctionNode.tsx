import { Aspect } from "@mimirorg/typelibrary-types";
import { Control, UseFormRegister } from "react-hook-form";
import { NodeFormAttributes } from "../attributes/NodeFormAttributes";
import { NodeFormTerminalTable } from "../terminals/NodeFormTerminalTable";
import { FormNodeLib } from "../types/formNodeLib";

export interface FunctionNodeProps {
  control: Control<FormNodeLib>;
  register: UseFormRegister<FormNodeLib>;
}

export const FunctionNode = ({ control, register }: FunctionNodeProps) => (
  <>
    <NodeFormTerminalTable control={control} />
    <NodeFormAttributes control={control} register={register} aspects={[Aspect.Function]} />
  </>
);
