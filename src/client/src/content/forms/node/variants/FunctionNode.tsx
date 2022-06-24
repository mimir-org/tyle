import { Aspect } from "@mimirorg/typelibrary-types";
import { Control, UseFormRegister } from "react-hook-form";
import { FormNodeLib } from "../../types/formNodeLib";
import { NodeFormAttributes } from "../attributes/NodeFormAttributes";
import { NodeFormTerminals } from "../terminals/NodeFormTerminals";
import { Flexbox } from "../../../../complib/layouts";
import { useTheme } from "styled-components";

export interface FunctionNodeProps {
  control: Control<FormNodeLib>;
  register: UseFormRegister<FormNodeLib>;
}

export const FunctionNode = ({ control, register }: FunctionNodeProps) => {
  const theme = useTheme();

  return (
    <Flexbox flexDirection={"column"} gap={theme.tyle.spacing.multiple(6)}>
      <NodeFormTerminals control={control} />
      <NodeFormAttributes control={control} register={register} aspects={[Aspect.Function]} />
    </Flexbox>
  );
};
