import { Flex } from "../../../../complib/layouts";
import { Spinner } from "../../../../complib/feedback";
import { TextResources } from "../../../../assets/text";
import { THEME } from "../../../../complib/core";
import { Heading } from "../../../../complib/text";

export const RegisterProcessing = () => {
  return (
    <Flex flexDirection={"column"} justifyContent={"center"} alignItems={"center"} gap={THEME.SPACING.MEDIUM}>
      <Heading as={"h2"}>{TextResources.REGISTER_PROCESSING}</Heading>
      <Spinner />
    </Flex>
  );
};
