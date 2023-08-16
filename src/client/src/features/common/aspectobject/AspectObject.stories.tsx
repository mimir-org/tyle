import { Meta, StoryFn } from "@storybook/react";
import { LibraryIcon } from "@mimirorg/component-library";
import { AspectObject } from "features/common/aspectobject/AspectObject";

export default {
  title: "Features/Common/AspectObject/AspectObject",
  component: AspectObject,
} as Meta<typeof AspectObject>;

const Template: StoryFn<typeof AspectObject> = (args) => <AspectObject {...args} />;

export const Default = Template.bind({});
Default.args = {
  name: "AspectObject",
  color: "#fef445",
  img: LibraryIcon,
};
