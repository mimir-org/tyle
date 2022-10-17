import { MotionFlexbox } from "../../../../../complib/layouts";
import { Heading, Text } from "../../../../../complib/text";

export interface UnauthenticatedContentHeaderProps {
  title?: string;
  subtitle?: string;
}

export const UnauthenticatedContentHeader = ({ title, subtitle }: UnauthenticatedContentHeaderProps) => {
  return (
    <MotionFlexbox as={"header"} flexDirection={"column"} layout>
      {title && (
        <Heading as={"h1"} variant={"display-small"}>
          {title}
        </Heading>
      )}
      {subtitle && <Text variant={"headline-small"}>{subtitle}</Text>}
    </MotionFlexbox>
  );
};
