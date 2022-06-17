import { useTheme } from "styled-components";
import { TextResources } from "../../../../assets/text";
import { Spinner } from "../../../../complib/feedback";
import { Flexbox } from "../../../../complib/layouts";
import { Heading } from "../../../../complib/text";

export const RegisterProcessing = () => {
  const theme = useTheme();

  return (
    <Flexbox flexDirection={"column"} justifyContent={"center"} alignItems={"center"} gap={theme.tyle.spacing.xl}>
      <Heading as={"h2"}>{TextResources.REGISTER_PROCESSING}</Heading>
      <Spinner />
    </Flexbox>
  );
};
