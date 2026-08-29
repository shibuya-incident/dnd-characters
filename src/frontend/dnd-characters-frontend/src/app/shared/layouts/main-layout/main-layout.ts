import { Component } from '@angular/core';
import { HeaderLayout } from '../header-layout/header-layout';
import { FooterLayout } from '../footer-layout/footer-layout';
import { PageLayout } from '../page-layout/page-layout';
import { Sidebar } from '../sidebar/sidebar';

@Component({
  imports: [HeaderLayout,FooterLayout,PageLayout,Sidebar],
  selector: 'app-main-layout',
  styleUrl: './main-layout.css',
  templateUrl: './main-layout.html',
})
export class MainLayout {}
