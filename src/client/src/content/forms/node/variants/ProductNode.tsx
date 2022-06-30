import { Aspect } from "@mimirorg/typelibrary-types";
import { Control, UseFormRegister } from "react-hook-form";
import { FormNodeLib } from "../../types/formNodeLib";
import { NodeFormAttributes } from "../attributes/NodeFormAttributes";
import { NodeFormTerminalTable } from "../terminals/NodeFormTerminalTable";

export interface ProductNodeProps {
  control: Control<FormNodeLib>;
  register: UseFormRegister<FormNodeLib>;
}

export const ProductNode = ({ control, register }: ProductNodeProps) => (
  <>
    <NodeFormTerminalTable control={control} />
    <NodeFormAttributes control={control} register={register} aspects={[Aspect.Product]} />
  </>
);
