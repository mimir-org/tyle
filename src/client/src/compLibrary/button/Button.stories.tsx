import { ComponentMeta } from "@storybook/react";
import { Default as Icon } from "../icon/Icon.stories";
import { Default as VisuallyHidden } from "../util/VisuallyHidden.stories";
import { Button } from "./Button";

export default {
  title: "Library/Atoms/Button",
  component: Button,
} as ComponentMeta<typeof Button>;

export const Default = () => <Button>{"Submit"}</Button>;

export const Disabled = () => <Button disabled>{"Submit"}</Button>;

export const AsAnchor = () => <Button as={"a"}>{"Submit"}</Button>;

export const WithIcon = () => (
  <Button>
    <Icon {...Icon.args} />
    <VisuallyHidden {...VisuallyHidden.args} />
  </Button>
);
