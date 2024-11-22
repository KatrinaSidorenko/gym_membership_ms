'use client';

import { setSportClasses } from '@slices/sportClass/sportClassSlice';
import {
  ExtendedSportClass,
  SportClasses,
} from '@serviceTypes/sportClassModels';
import { Spinner } from '@nextui-org/spinner';
import AdminDashboard from '@/components/dashboard/admin/adminDashboard';
import React, { useEffect } from 'react';
import { useAppDispatch, useAppSelector } from '@hooks/reduxHooks';
import { selectUser } from '@slices/user/userSlice';
import { useGetAllActiveClassesQuery } from '@services/sportClasses/sportClassService';

export default function Dashboard({ params }: { params: { role: string } }) {
  const dispatch = useAppDispatch();
  const { data, isLoading, error } = useGetAllActiveClassesQuery();
  console.log(data);
  const role = React.use(params).role;
  console.log(role);
  // Dispatch classes only after data is loaded
  useEffect(() => {
    if (data && !isLoading && !error) {
      dispatch(setSportClasses(data as SportClasses));
    }
  }, [data, isLoading, error, dispatch]);

  if (isLoading) {
    return <Spinner />;
  }

  const content = role === "Admin" ? <AdminDashboard sportClasses={data as ExtendedSportClass[]} />: null;

  return content ?? <div className="justify-center">
    <h1>No classes available</h1>
  </div>;
}
