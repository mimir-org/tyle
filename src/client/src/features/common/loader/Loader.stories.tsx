import { ComponentStory } from "@storybook/react";
import { Loader } from "features/common/loader/Loader";

export default {
  title: "Features/Common/Loader",
  component: Loader,
};

const Template: ComponentStory<typeof Loader> = () => <Loader />;

export const Default = Template.bind({});
