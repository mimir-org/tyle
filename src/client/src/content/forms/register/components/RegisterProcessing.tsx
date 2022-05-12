import { Flexbox } from "../../../../complib/layouts";
import { Spinner } from "../../../../complib/feedback";
import { TextResources } from "../../../../assets/text";
import { Heading } from "../../../../complib/text";
import { useTheme } from "styled-components";

export const RegisterProcessing = () => {
  const theme = useTheme();

  return (
    <Flexbox flexDirection={"column"} justifyContent={"center"} alignItems={"center"} gap={theme.tyle.spacing.medium}>
      <Heading as={"h2"}>{TextResources.REGISTER_PROCESSING}</Heading>
      <Spinner />
    </Flexbox>
  );
};
