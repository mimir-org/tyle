import { Flexbox } from "../../../../complib/layouts";
import { Spinner } from "../../../../complib/feedback";
import { TextResources } from "../../../../assets/text";
import { theme } from "../../../../complib/core";
import { Heading } from "../../../../complib/text";

export const RegisterProcessing = () => {
  return (
    <Flexbox flexDirection={"column"} justifyContent={"center"} alignItems={"center"} gap={theme.spacing.medium}>
      <Heading as={"h2"}>{TextResources.REGISTER_PROCESSING}</Heading>
      <Spinner />
    </Flexbox>
  );
};
